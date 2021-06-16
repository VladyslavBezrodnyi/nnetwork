import React from 'react';
import { BrowserRouter, Route, Switch, Link } from 'react-router-dom';
import './App.css';
import { Layout, Menu, Space, Typography, Button } from 'antd';
import "../../../node_modules/antd/dist/antd.min.css";
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import HeaderContainer from '../Header/Header';
import HomeContainer from '../Home/Home';
import CreateNetworkContainer from '../CreateNetwork/CreateNetwork';
import NetworkInfoContainer from '../NetworkInfo/NetworkInfo';
import NetworksContainer from '../Networks/Networks';
import { AppProps } from '../../types/props-types';
import {
  SearchOutlined,
  FileTextOutlined,
  PlusOutlined,
} from '@ant-design/icons'
import { Image } from 'antd';
import logo from '../../images/logo.png';

const { Header, Sider, Content, Footer } = Layout;
const { Text } = Typography;

class App extends React.PureComponent<AppProps>{
  state = {
    collapsed: false,
    current: "1",
  };

  constructor(props: AppProps) {
    super(props);
  }

  onCollapse = (collapsed: any) => {
    console.log(collapsed);
    this.setState({ collapsed });
  };


  onClickMenuItem = (e: any) => {
    this.setState({
      current: e.key
    })
  }

  getMenuItems = (isDark: boolean, isInline: boolean) => {
    return (
      <Menu theme={(isDark) ? ("dark") : ("light")}
        mode={(isInline) ? ("inline") : ("horizontal")}
        defaultSelectedKeys={[this.state.current]}
        onClick={this.onClickMenuItem}>
        <Menu.Item key="1" icon={<FileTextOutlined />}>
          <Link to="/">Home</Link>
        </Menu.Item>
        <Menu.Item key="2" icon={<PlusOutlined />}>
          <Link to="/create">Create</Link>
        </Menu.Item>
        <Menu.Item key="3" icon={<SearchOutlined />}>
          <Link to="/networks">Networks</Link>
        </Menu.Item>
      </Menu>
    )
  }

  getMenuSider = () => {
    return (
      <Sider collapsible collapsed={this.state.collapsed} onCollapse={this.onCollapse}>
        <div className="logo">
          <Image
            height={75}
            width={200}
            src={logo}
            alt={"logo"}
          />
        </div>
        {this.getMenuItems(true, true)}
      </Sider>
    );
  }

  render() {
    return (
      <BrowserRouter>
        <Layout style={{ minHeight: '100vh' }}>
          {this.getMenuSider()}
          <Layout className="site-layout">
            <HeaderContainer />
            <Content className="site-layout-background"
              style={{
                margin: '24px 16px',
                padding: 24,
                minHeight: '60%'
              }}>
              <div className="site-layout-background" style={{ padding: 24, }}>
                <Switch>
                  <Route exact path="/" component={HomeContainer} />
                  <Route exact path="/create" component={CreateNetworkContainer} />
                  <Route exact path="/networks" component={NetworksContainer} />
                  <Route path="/networks/:id" component={NetworkInfoContainer} />
                </Switch>
              </div>
            </Content>
          </Layout>
        </Layout>
      </BrowserRouter>
    )
  }
}

const mapStateToProps = (state: any) => {
  return {
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
  }, dispatch)
}

const AppContainer = connect(mapStateToProps, mapDispatchToProps)(App);
export default AppContainer;
