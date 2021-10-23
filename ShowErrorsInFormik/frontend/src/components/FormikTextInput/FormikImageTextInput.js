import classNames from "classnames";
import { useState } from "react";
import { useSelector,useDispatch } from "react-redux";
import './../../css/index.css';


const FormikImageTextInput = ({refFormik, ...props}) => 
{
    const [srcImg, setSrc] = useState("/images/program/no-image.png");
    const auth = useSelector(redux => redux.auth);
    const dispatch = useDispatch();
    
    const onDrag = (e) => {
        e.preventDefault();
        dispatch({type: "IS_DRAG"});
    }

    const outDrop = (e) => {
        e.preventDefault();
        dispatch({type: "OUT_DRAG"});
    }

    const onChangeFileHandler = (e) => {
        e.preventDefault();
        dispatch({type: "OUT_DRAG"});

        var file;
        if(e.target.files) 
        {
            file = e.target.files[0];
        }else if(e.dataTransfer) 
        {
            file = e.dataTransfer.files[0];
        }

        if(file !== 'undefined') 
        {
            var source = URL.createObjectURL(file);
            setSrc(source);
            refFormik.current.setFieldValue('image', file);
        }
    }

    return (
    <div className={classNames("form-group", "mt-3")}>
        <div className="container">
            <div className="row">
                <div className="offset-md-4 col-md-4">
                    <label htmlFor={props.id || props.name} className="d-flex justify-content-center">
                        <img src={srcImg} width="200" height="200"
                        onDragOver={onDrag} onDrop={onChangeFileHandler} onDragLeave={outDrop} onDragEnter={onDrag}
                         className={classNames(
                            {"default": !auth.isDrag }, {"dragIn": auth.isDrag})}/>
                    </label>

                    <input type="file" className="d-none" {...props} onChange={onChangeFileHandler}/>
                </div>
            </div>
        </div>
    </div>);
}

export default FormikImageTextInput;