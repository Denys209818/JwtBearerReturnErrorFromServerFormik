import classNames from "classnames";
import { useField } from "formik";
import { useState } from "react";
import {useIMask} from 'react-imask';

const FormikPhoneTextInput = ({label, ...props}) => {

    var [opts, setOpts] = useState({mask: '+00(000) 000 00 00'});
    var {ref, maskRef} = useIMask(opts);

    const [field, meta] = useField(props);
    return (
        <div className="form-group">
            <label htmlFor={props.id || props.name} className="form-label">{label}</label>
            <input ref={ref} {...field} {...props} className={classNames("form-control", {"is-valid": (!meta.error && meta.touched)},
            {"is-invalid": (meta.error && meta.touched)})} />

            {meta.error && <div className="invalid-feedback">
                    {meta.error}
                </div>}
        </div>
    );
}

export default FormikPhoneTextInput;