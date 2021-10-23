import * as Yup from 'yup';

export default Yup.object({
    firstname: Yup.string().required('Поле обовязкове для заповнення!'),
    secondname: Yup.string().required('Поле обовязкове для заповнення!'),
    phone: Yup.string()
    .matches(/^(?=\+?([0-9]{2})\(?([0-9]{3})\)\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})).{18}$/, 
    'Номер введено не коректно!').required("Поле має бути обовязковим!"),
    email: Yup.string().email('Не коректна електронна пошта!').required('Поле обовязкове для заповнення!'),
    password: Yup.string().required('Поле обовязкове для заповнення!').min(5, 'Мінімальна кількість символів - 5!'),
    confirmPassword: Yup.string().required('Поле обовязкове для заповнення!').oneOf([Yup.ref('password'), null], 'Поля не співпадають!')
});