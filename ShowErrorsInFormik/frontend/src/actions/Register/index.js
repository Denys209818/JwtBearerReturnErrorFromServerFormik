import axiosService from "../../services/axiosService";


const RegisterAction = (values, auth) => (dispatch) => {
    try {

        var formData = new FormData();

        Object.entries(values).forEach(([key, value]) => {
            formData.append(key, value)
        });

        return axiosService.send('api/account/registeruser', formData, auth);
    }
    catch (err) {
        console.log(err.response.data.errors);
        return Promise.reject(err.response.data.errors);
    }
}

export default RegisterAction;