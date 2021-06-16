import React from 'react';
import { Button, Input, Form, message, Space, InputNumber, Divider, Select, Card, Modal, Image } from 'antd';
import { CreateNetworkProps } from '../../types/props-types';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { LayerDto } from '../../types/dto-types';
import { LayerType } from '../../enums/layer-type';
import form, { FormInstance } from 'antd/lib/form';
import layerService from '../../services/layer-service';
import { createNetwork } from '../../redux/actions/create-network-actions';
import "./CreateNetwork.css";
import model from "../../images/model1.png";

const { Option } = Select;

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};

const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};

const layerTypes = Object.keys(LayerType).map((item: string | LayerType, i: Number) => {
    return { label: item, value: item };
})

const getParameterFields = (name: number, fieldKey: number, layerType: LayerType): any => {
    console.log(layerType);
    return layerService(layerType).map((item: string) => {

        switch (item) {
            case "shape": {
                return <Input.Group compact>
                    <Form.Item
                        name={[name, 'parameters', item, 0]}
                        fieldKey={[fieldKey, 'parameters', item, 0]}
                        label={item}
                        rules={[{ type: 'number', min: 1, max: 1000 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                    <Form.Item
                        name={[name, 'parameters', item, 1]}
                        fieldKey={[fieldKey, 'parameters', item, 1]}
                        label=" "
                        rules={[{ type: 'number', min: 1, max: 1000 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                    <Form.Item
                        name={[name, 'parameters', item, 2]}
                        fieldKey={[fieldKey, 'parameters', item, 2]}
                        label=" "
                        rules={[{ type: 'number', min: 1, max: 1000 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                </Input.Group>;
            }
            case "kernel_size":
            case "size":
            case "pool_size": {
                return <Input.Group compact>
                    <Form.Item
                        name={[name, 'parameters', item, 0]}
                        fieldKey={[fieldKey, 'parameters', item, 0]}
                        label={item}
                        rules={[{ type: 'number', min: 1, max: 100 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                    <Form.Item
                        name={[name, 'parameters', item, 1]}
                        fieldKey={[fieldKey, 'parameters', item, 1]}
                        label="  "
                        rules={[{ type: 'number', min: 1, max: 100 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                </Input.Group>
            }
            case "filters":
            case "alpha":
            case "units":
            case "rate":
            case "axis":
                {
                    return <Form.Item
                        name={[name, 'parameters', item]}
                        fieldKey={[fieldKey, 'parameters', item]}
                        label={item}
                        rules={[{ type: 'number', min: -100, max: 100 }]}
                    >
                        <InputNumber />
                    </Form.Item>
                }
            default: {
                return <></>;
            }
        }

    })
}

class CreateNetwork extends React.PureComponent<CreateNetworkProps, any>{
    state = {
        layerTypes: layerTypes,
        visible: false,
    };

    formRef = React.createRef<FormInstance>();

    constructor(props: any) {
        super(props);
    }

    onFinish = (values: any) => {
        console.log(values);
        this.props.createNetwork(values);
        this.showModal();
    };

    showModal = () => {
        this.setState({
            visible: true,
        });
    };

    handleCancel = (e: any) => {
        console.log(e);
        this.setState({
            visible: false,
        });
    };

    resetFields = (name: number) => (newValue: any) => {
        this.formRef.current?.resetFields([
            ['layers', name, 'transitions'],
            ['layers', name, 'parameters'],
        ])
    }

    getInitStates = () => {
        let optionKey = 0;
        return (
            <Form
                name="network"
                onFinish={this.onFinish}
                autoComplete="off"
                ref={this.formRef}
            >
                <Space direction="vertical" className="border">
                    <Form.Item name={"title"} label={"Network Name"}>
                        <Input />
                    </Form.Item>
                    <Form.Item name={"description"} label={"Description"}>
                        <Input />
                    </Form.Item>
                </Space>
                <Form.List name="layers">
                    {(fields, { add, remove }) => {
                        //console.log(fields);
                        return (
                            <>
                                {fields.map(({ key, name, fieldKey, ...restField }) => {
                                    //console.log({ fields, key, name, fieldKey, ...restField });
                                    //console.log(this.state.layers[name].layerType);
                                    return (
                                        <>
                                            <Divider key={`"${name}" Layer`}>
                                                {`"${name + 1}" Layer`}
                                                <br />
                                                {(name + 1 === optionKey) ?
                                                    (<MinusCircleOutlined
                                                        onClick={() => {
                                                            optionKey--;
                                                            //console.log(this.state.layers)
                                                            remove(name)
                                                        }}
                                                    />) :
                                                    (<></>)
                                                }
                                            </Divider>
                                            <Space
                                                direction="vertical"
                                            >
                                                <Card 
                                                    style={{ width: 600 }}
                                                    className="border"
                                                >
                                                    <Form.Item
                                                        {...restField}
                                                        name={[name, 'id']}
                                                        fieldKey={[fieldKey, 'id']}
                                                        hidden={true}
                                                        initialValue={name}
                                                    >
                                                        <Input type={"number"} />
                                                    </Form.Item>
                                                    <Form.Item
                                                        {...restField}
                                                        name={[name, 'layerType']}
                                                        fieldKey={[fieldKey, 'layerType']}
                                                        rules={[{ required: true, message: "is required" }]}
                                                        label="Layer Type"
                                                    >
                                                        <Select
                                                            allowClear
                                                            onChange={this.resetFields(name)}
                                                            style={{ width: 200 }}
                                                            options={this.state.layerTypes}
                                                        />
                                                    </Form.Item>

                                                    <Form.Item
                                                        {...restField}
                                                        noStyle
                                                        shouldUpdate={(prevValues, curValues) =>
                                                            prevValues?.layers[name]?.layerType !== curValues?.layers[name]?.layerType
                                                        }
                                                    >
                                                        {({ getFieldValue }) => {
                                                            var layerType = getFieldValue(['layers', name, 'layerType']);
                                                            return <>
                                                                {getParameterFields(name, fieldKey, layerType)}
                                                                {(layerType !== "Input" && layerType) ? (
                                                                    <>
                                                                    <div>Transition from: </div>
                                                                    <Form.Item
                                                                        {...restField}
                                                                        name={[name, 'transitions']}
                                                                        fieldKey={[fieldKey, 'transitions']}
                                                                        noStyle
                                                                        shouldUpdate={true}
                                                                        label="Transitions"
                                                                    >
                                                                        <Select
                                                                            mode="multiple"
                                                                            style={{ width: 200 }}
                                                                            allowClear
                                                                            placeholder="Please select"
                                                                        >
                                                                            {fields.map(field => (<Select.Option value={field.name + 0}>{field.name + 1}</Select.Option>))}
                                                                        </Select>
                                                                    </Form.Item>
                                                                    </>
                                                                ) : (<></>)}
                                                            </>
                                                        }}
                                                    </Form.Item>
                                                </Card>
                                            </Space>
                                        </>
                                    )
                                })}
                                <Form.Item>
                                    <Button
                                        style={{ margin: '30px' }}
                                        type="dashed"
                                        onClick={() => {
                                            //console.log(this.state.layers)
                                            optionKey++;
                                            add();
                                        }}
                                        block
                                        icon={<PlusOutlined />}>
                                        Add Layer
                                    </Button>
                                </Form.Item>
                            </>
                        )
                    }}
                </Form.List>
                <Button type="primary" htmlType="submit">Submit</Button>
            </Form>
        );
    }

    redirectToHome = () => {
        this.props.history.push(`/`)
    }

    render() {
        return (
            <>
                <div>
                    {this.getInitStates()}
                </div>
                <Modal
                    title={"Creation result"}
                    visible={this.state.visible}
                    onCancel={this.handleCancel}

                >
                    {
                    (this.props?.networkCreationResult?.plotImage)? (
                        <Image src={`data:image/jpeg;base64, ${this.props.networkCreationResult.plotImage}`}/>
                    ):(<></>)
                    }
                </Modal>
            </>
        )
    }
}

const mapStateToProps = (state: any) => {
    return {
        networkCreationResult: state.createNetwork.networkCreationResult
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        createNetwork: createNetwork,
    }, dispatch)
}

const CreateNetworkContainer = withRouter(connect(mapStateToProps, mapDispatchToProps)(CreateNetwork as any));
export default CreateNetworkContainer;