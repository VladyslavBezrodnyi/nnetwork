import {
    CREATE_NETWORK,
} from '../../constants/create-network-constants';
import { message } from 'antd';
import axios from 'axios';
import { environment } from '../../environment/environment';
import { NetworkInitializerDto } from '../../types/dto-types';


export const createNetwork = (networkInitializer: NetworkInitializerDto) => (despatch: any) => {
    console.log(networkInitializer);
    axios.post(environment.API_URL + '/api/Network/create', {
        ...networkInitializer
    }).then((response: any) => {
        message.success("Success!")
        console.log(response.data)
        despatch({
            type: CREATE_NETWORK,
            payload: {
                networkCreationResult: response.data
            }
        });
    }).catch((err: any) => {
        message.warning("Warning!");
        console.log(err);
    })
}