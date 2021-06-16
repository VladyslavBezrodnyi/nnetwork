import { NetworkDto } from './../../types/dto-types';
import { NetworksState } from '../../types/state-types';
import { Action } from '../../types/common-types';
import { GET_ALL_NETWORKS } from '../../constants/networks-constants';

const initialState = {
    networks: [
       /* {
            id: "0",
            title: "CNN Network",
            description: "CNN"
        } as NetworkDto,
        {
            id: "1",
            title: "CIFAR10 Network",
            description: "CIFAR10"
        } as NetworkDto,
        {
            id: "2",
            title: "Network1",
            description: "net"
        } as NetworkDto,
        {
            id: "3",
            title: "Network2",
            description: ""
        } as NetworkDto
        */
    ]
} as NetworksState

const networksReducer = (state: NetworksState = initialState, action: Action): NetworksState => {
    switch (action.type) {
        case GET_ALL_NETWORKS:
            return {
                ...state,
                networks : action?.payload?.networks
            }
        default:
            return {
                ...state
            }
    }
}
export default networksReducer;