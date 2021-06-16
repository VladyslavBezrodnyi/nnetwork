import React from 'react';
import { Layout, Menu, Button, Row, Col, Form, Modal, Input, Divider, List, Typography } from 'antd';
import { NetworksProps } from '../../types/props-types';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { NetworkDto } from '../../types/dto-types';
import { getAllNetworks } from '../../redux/actions/networks-actions';

const { Text } = Typography;

let layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
let tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

class Networks extends React.PureComponent<NetworksProps, any>{
  state = { visible: false };

  constructor(props: NetworksProps) {
    super(props);
    this.props.getAllNetworks();
  }

  onOpenNetwork = (id: string) => {

  }

  render() {
    return (
      <div>
        <Divider>Networks</Divider>
        <List
          itemLayout="horizontal"
          dataSource={this.props.networks}
          renderItem={(item: NetworkDto, i: number) => (
            <List.Item
              actions={[
                <Button 
                  type="primary" 
                  key={`${i}-network`} 
                  onClick={() => { this.onOpenNetwork(item.id) }}
                >
                  <Link to={`/networks/${item.id}`}>Open Info</Link>
                </Button>,
              ]}
            >
              <List.Item.Meta title={<Text strong>{`${item.title}`}</Text>} />
              <List.Item.Meta description={<Text>{`${item.description}`}</Text>} />
            </List.Item>
          )}
        />
      </div>
    );
  }
}

const mapStateToProps = (state: any) => {
  return {
    networks: state.networks.networks
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    getAllNetworks: getAllNetworks
  }, dispatch)
}

const NetworksContainer = withRouter(connect(mapStateToProps, mapDispatchToProps)(Networks as any));
export default NetworksContainer;