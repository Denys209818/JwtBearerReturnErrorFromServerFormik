import { useSelector } from "react-redux";

const Main = () => {
    var auth = useSelector(redux => redux.auth);
    console.log(auth);
    return (<div><h1>Main</h1></div>);
}

export default Main;