import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import Navbar from './components/Navbar';
import { BrowserRouter as Router,
Switch, Route } from 'react-router-dom';
import Register from './components/Register';
import { Provider } from 'react-redux';
import store, { history } from './redux/store';
import { ConnectedRouter } from 'connected-react-router';
import Main from './components/Main';


const App = () => {

  return (
    <Provider store={store}>
<ConnectedRouter history={history}>
      <Navbar/>

      <Switch>
        <Route exact path="/">
          <Main/>
        </Route>
        <Route exact path="/register">
          <Register/>
        </Route>
      </Switch>
</ConnectedRouter>
    </Provider>
  );
}

export default App;
