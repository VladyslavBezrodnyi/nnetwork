import React from 'react';
import { Layout, Menu, Button, Row, Col, Form, Modal, Input, Divider, List, Typography, Image, message, Space, Descriptions, Upload, Card } from 'antd';
import { NetworkInfoProps } from '../../types/props-types';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { NetworkDto, PredictionDto } from '../../types/dto-types';
import { getNetworkById, predictImage } from '../../redux/actions/network-actions';
import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { environment } from '../../environment/environment';
import { PredictionResultDto } from '../../types/dto-types';
import "./NetworkInfo.css"

const { Text } = Typography;

let layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
let tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

const classes = ["airplane", "automobile", "bird", "cat", "deer", "dog", "frog", "horse", "ship", "truck"]

function getBase64(img: any, callback: any) {
  const reader = new FileReader();
  reader.addEventListener('load', () => callback(reader.result));
  reader.readAsDataURL(img);
}

function beforeUpload(file: any) {
  const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
  if (!isJpgOrPng) {
    message.error('You can only upload JPG/PNG file!');
  }
  const isLt2M = file.size / 1024 / 1024 < 2;
  if (!isLt2M) {
    message.error('Image must smaller than 2MB!');
  }
  return isJpgOrPng && isLt2M;
}

class NetworkInfo extends React.PureComponent<NetworkInfoProps, any>{
  state = {
    visible: false,
    imageUrl: null,
    loading: false,
    prediction: {
      index: -1,
      probabilities: classes.map(item => 0)
    } as PredictionResultDto
  };

  constructor(props: NetworkInfoProps) {
    super(props);
    const networkId = this.props.match.params.id;
    this.props.getNetworkById(networkId);
  }

  onOpenNetwork = (id: string) => {

  }

  handleChange = (info: any) => {
    if (info.file.status === 'uploading') {
      this.setState({ loading: true });
      return;
    }
    if (info.file.status === 'done') {
      getBase64(info.file.originFileObj, (imageUrl: any) => {
        this.setState({ prediction: info.file.response });
        return this.setState({
          imageUrl,
          loading: false,
        });
      });
    };
  };

  render() {
    const { loading, imageUrl } = this.state;
    const uploadButton = (
      <div>
        {loading ? <LoadingOutlined /> : <PlusOutlined />}
        <div style={{ marginTop: 8 }}>Upload</div>
      </div>
    );
    return (
      <div>
        <Descriptions title="Network Info" className="border" bordered>
          <Descriptions.Item label={<Text strong>Title:</Text>}>
            <Text mark>{this.props.network.title}</Text>
          </Descriptions.Item>
          <Descriptions.Item label={<Text strong>Description:</Text>}>
            <Text mark>{this.props.network.description}</Text>
          </Descriptions.Item>
          <Descriptions.Item label={<Text strong>Config Info:</Text>}>
            <Image
              style={{ maxWidth: 300, maxHeight: 300 }}
              src={`data:image/jpeg;base64, ${this.props.network.plotImage}`}
            />
          </Descriptions.Item>
        </Descriptions>,
        <Upload
          name="avatar"
          listType="picture-card"
          className="avatar-uploader"
          showUploadList={false}
          action={`${environment.API_URL}/api/network/predict/${this.props.match.params.id}`}
          beforeUpload={beforeUpload}
          onChange={this.handleChange}
        >
          {imageUrl ?
            <Image
              style={{ width: '100%' }}
              src={`${imageUrl}`}
              alt="avatar"
            />
            : uploadButton}
        </Upload>
        <Card title="Classes" className="border" >
          {this.state.prediction.probabilities.map((item, index) => {
            return (
              <Card.Grid style={{ width: '20%', textAlign: 'center' }}>
                <Text type={(index === this.state.prediction.index) ? ("success") : ("danger")}>
                  {classes[index]}
                </Text>
                <br />
                {item}
              </Card.Grid>);
          })}
        </Card>
      </div>
    );
  }
}

const mapStateToProps = (state: any) => {
  return {
    network: state.network.network
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    getNetworkById: getNetworkById,
    predictImage: predictImage
  }, dispatch)
}

const NetworkInfoContainer = withRouter(connect(mapStateToProps, mapDispatchToProps)(NetworkInfo as any));
export default NetworkInfoContainer;