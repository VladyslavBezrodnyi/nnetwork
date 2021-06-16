import React from 'react'
import { connect } from 'react-redux'
import { FormattedMessage } from 'react-intl'
import Text from 'antd/lib/typography/Text'
import { Space, Divider, Alert } from 'antd'
import { bindActionCreators } from 'redux'
import { HomeProps } from '../../types/props-types'
import { Typography } from 'antd';

const { Title } = Typography;

class Home extends React.PureComponent<HomeProps>{
    render() {
        return (
            <>
            </>
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

const HomeContainer = connect(mapStateToProps, mapDispatchToProps)(Home)
export default HomeContainer;