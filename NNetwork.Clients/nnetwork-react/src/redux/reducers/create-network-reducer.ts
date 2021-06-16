import { CREATE_NETWORK } from '../../constants/create-network-constants';
import { Action } from '../../types/common-types';
import { NetworkCreationResultDto } from '../../types/dto-types';
import { CreateNetworkState } from '../../types/state-types';

const initialState = {
    networkCreationResult: {plotImage: ""} as NetworkCreationResultDto
} as CreateNetworkState

const createNetworkReducer = (state: CreateNetworkState = initialState, action: Action): CreateNetworkState => {
    switch (action.type) {
        case CREATE_NETWORK:
            return {
                ...state,
                networkCreationResult : action?.payload.networkCreationResult
            }
        default:
            return {
                ...state
            }
    }
}
export default createNetworkReducer;