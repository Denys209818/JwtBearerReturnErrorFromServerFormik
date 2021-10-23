import { useSelector } from "react-redux";
import axiosCreate from "./axiosCreate"


class axiosService {
    send = (url, data, auth) => {

        return axiosCreate.post(url, data, {
            headers: {
                'Content-Type': 'multipart/form-data',
                Authorization: "Bearer " + auth.token
            }
        });
    }
}

export default new axiosService();