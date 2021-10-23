import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const Navbar = () => {
    var auth = useSelector(redux => redux.auth);
    return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">
                <Link to="/" className="navbar-brand">Jwt Bearer</Link>
                <button className="navbar-toggler" data-bs-target="#mainMenu" data-bs-toggle="collapse">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div id="mainMenu" className="collapse navbar-collapse">
                    <ul className="navbar-nav me-auto">
                        <li className="nav-item">
                            <Link to="/" className="nav-link">Головна сторінка</Link>
                        </li>
                    </ul>
                    {!auth.isAuth &&  <ul className="navbar-nav">
                        <li className="nav-item">
                            <Link to="/register" className="nav-link">Зареєструватися</Link>
                        </li>
                    </ul>}

                    {auth.isAuth && <ul className="navbar-nav">
                    <li className="nav-item">
                            <Link to="/" className="nav-link">Привіт, {auth.firstname}</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/" className="nav-link">Вихід</Link>
                        </li>
                    </ul>}
                    
                </div>
            </div>
    </nav>);
}

export default Navbar;