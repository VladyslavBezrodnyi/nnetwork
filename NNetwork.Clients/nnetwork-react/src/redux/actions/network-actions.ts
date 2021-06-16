import { GET_NETWORK_BY_ID, GET_PREDICTION_RESULT, } from './../../constants/networks-constants';
import { message, notification } from 'antd';
import axios from 'axios';
import { environment } from './../../environment/environment';
import { PredictionDto } from '../../types/dto-types';


export const getNetworkById = (networkId: string) => (despatch: any) => {
    axios.get(environment.API_URL + '/api/network/info/' + networkId).then((response: any) => {
        message.success("Success!")
        despatch({
            type: GET_NETWORK_BY_ID,
            payload: {
                network: response.data
            }
        });
    })
}

export const predictImage = (predictionDto: PredictionDto) => (despatch: any) => {
    axios.post(environment.API_URL + '/api/network/predict/' + predictionDto.networkId,
    {
        ...predictionDto
    }).then((response: any) => {
        message.success("Success!")
        despatch({
            type: GET_PREDICTION_RESULT,
            payload: {
                network: response.data
            }
        });
    })
}