import { applyMiddleware, combineReducers, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import authReducer  from './reducers/authReducer';
import thunk from "redux-thunk";
import { connectRouter, routerMiddleware } from "connected-react-router";
import {createBrowserHistory} from 'history';

var dataHistory = document.getElementsByTagName('base')[0].getAttribute('href');
export const history = createBrowserHistory({basename: dataHistory});

var middleware = [thunk,
routerMiddleware(history)];



var rootReducer = combineReducers({
    auth: authReducer,
    router: connectRouter(history)
});

var store = createStore(rootReducer, {}, 
    composeWithDevTools(applyMiddleware(...middleware)));

export default store;