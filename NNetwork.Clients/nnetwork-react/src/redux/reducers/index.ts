import { combineReducers } from 'redux';
import createNetworkReducer from './create-network-reducer';
import networksReducer from './networks-reducer';
import networkReducer from './network-reducer';

const rootReducer = combineReducers({
    createNetwork: createNetworkReducer,
    networks: networksReducer,
    network: networkReducer
});

export default rootReducer;