import { push } from "connected-react-router";
import { Form, Formik } from "formik";
import { useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import RegisterAction from "../../actions/Register";
import axiosService from "../../services/axiosService";
import Loader from "../common/loader";
import FormikTextInput from "../FormikTextInput";
import FormikImageTextInput from "../FormikTextInput/FormikImageTextInput";
import FormikPhoneTextInput from "../FormikTextInput/FormikPhoneTextInput";
import yupValidationRegister from "./yupValidationRegister";
import './../../css/loader.css';


const Register = () => {
    var initialValues = {
        firstname: '',
        secondname: '',
        email: '',
        password: '',
        phone: '',
        confirmPassword:'',
        image: null
    };

    var refFormik = useRef();
    var hRef = useRef();
    var auth = useSelector(redux => redux.auth);

    var dispatch = useDispatch();

    const onSubmitHandler = (values, {setFieldError}) => 
    {
        dispatch({type: "START_LOADER"});
        var result = dispatch(RegisterAction(values, auth));

            result.then(resolve => {
                console.log(resolve.data);
                dispatch({type: "REGISTER", payload: resolve.data});
                dispatch({type: "END_LOADER"});
                dispatch(push("/"));
            }, reject => {
                Object.entries(reject.response.data.errors).forEach(([key,value]) => {
                    setFieldError(key, value);
                });
    
                hRef.current.scrollIntoView({ behavior: 'smooth' });
                dispatch({type: "END_LOADER"});
            });
       
    }

    return (
        
        <div className="container mt-4">
            {auth.isLoad &&
               <Loader/>
            }
            <div className="row">
                <div className="offset-md-3 col-md-6">
                    <h1 className="text-center" ref={hRef}>Реєстрація</h1>
                    <Formik
                        initialValues={initialValues}
                        validationSchema={yupValidationRegister}
                        onSubmit={onSubmitHandler}
                        innerRef={refFormik}
                    >
                        <Form>
                            <FormikTextInput
                            name="firstname"
                            id="firstname"
                            type="text"
                            label="Ім'я"
                            />
                             <FormikTextInput
                            name="secondname"
                            id="secondname"
                            type="text"
                            label="Прізвище"
                            />
                             <FormikTextInput
                            name="email"
                            id="email"
                            type="text"
                            label="Електронна пошта"
                            />
                            <FormikPhoneTextInput
                            name="phone"
                            id="phone"
                            type="text"
                            label="Телефон"
                            />
                             <FormikTextInput
                            name="password"
                            id="password"
                            type="password"
                            label="Пароль"
                            />
                            <FormikTextInput
                            name="confirmPassword"
                            id="confirmPassword"
                            type="password"
                            label="Підтверіть пароль"
                            />

                            <FormikImageTextInput
                                id="image"
                                name="image"
                                refFormik={refFormik}
                            />

                            <input type="submit" className="btn btn-success mt-4" value="Зареєструватися"/>
                        </Form>
                    </Formik>
                </div>
            </div>
        </div>
    );
}

export default Register;