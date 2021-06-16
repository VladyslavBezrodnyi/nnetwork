import { GET_ALL_NETWORKS } from './../../constants/networks-constants';
import { message, notification } from 'antd';
import axios from 'axios';
import { environment } from './../../environment/environment';
import { NetworkDto } from '../../types/dto-types';

export const getAllNetworks = () => (despatch: any) => {
    axios.get(environment.API_URL + '/api/network/all').then((response: any) => {
        despatch({
            type: GET_ALL_NETWORKS,
            payload: {
                networks: response.data as NetworkDto[]
            }
        });
    }).catch((err) => {
        console.log(err);
        notification.open({
            message: err.toString()
        });
    })
};