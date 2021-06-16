import React from 'react';
import { Layout, Menu, Button, Row, Col, Form, Modal, Input } from 'antd';
import "./Header.css";
import { HeaderProps } from '../../types/props-types';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'

let layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
let tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

class Header extends React.PureComponent<HeaderProps>{
  state = { visible: false };

  render() {
    return (
      <div>
        <Layout.Header className="site-layout-background" style={{ padding: 0 }}>
        </Layout.Header>
      </div>
    );
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

const HeaderContainer = withRouter(connect(mapStateToProps, mapDispatchToProps)(Header as any));
export default HeaderContainer;