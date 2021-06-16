import { NetworkState } from '../../types/state-types';
import { Action } from '../../types/common-types';
import { GET_NETWORK_BY_ID, GET_PREDICTION_RESULT } from '../../constants/networks-constants';
import { NetworkInfoDto, PredictionResultDto } from '../../types/dto-types';

const initialState = {
    network: {} as NetworkInfoDto
} as NetworkState

const networkReducer = (state: NetworkState = initialState, action: Action): NetworkState => {
    switch (action.type) {
        case GET_NETWORK_BY_ID:
            return {
                ...state,
                network : action?.payload?.network as NetworkInfoDto
            }
        case GET_PREDICTION_RESULT:
            console.log(action?.payload)
            return {
                ...state,
                predictionResult : action?.payload as PredictionResultDto
            }
        default:
            return {
                ...state
            }
    }
}
export default networkReducer;