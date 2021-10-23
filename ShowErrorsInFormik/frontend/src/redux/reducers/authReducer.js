
var initState = {
    isDrag: false,
    token: '',
    firstname:'',
    email: '',
    isAuth: false,
    isLoad: false
}

const authReducer = (state = initState, action) => {

    switch(action.type) 
    {
        case "IS_DRAG": 
        {
            return {
                ...state,
                isDrag: true
            }
        }
        case "OUT_DRAG": 
        {
            return {
                ...state,
                isDrag: false
            }
        }
        case "REGISTER": {
            var data = action.payload;
            return {
                ...state,
                token: data.token,
                firstname: data.firstname,
                email: data.email,
                isAuth: true
            }
        }
        case "START_LOADER": 
        {
            return {
                ...state,
                isLoad: true
            }
        }
        case "END_LOADER": 
        {
            return {
                ...state,
                isLoad: false
            }
        }
    }

    return state;
}

export default authReducer;