import React from 'react';
import { connect } from 'react-redux';
import { getUserFromLocalStorage } from './util';
import { setUser, setIsLoggedIn } from './store/actions/app';
import Login from './login/Login';
import Dashboard from './dashboard/Dashboard';
import 'bulma/css/bulma.css'


class AuthHOC extends React.Component {
    isLoggedin = () => {
        let flag = this.props.isLoggedIn;
        if (!flag) {
            const user = getUserFromLocalStorage();
            if (user) {
                flag = true;
                this.props.setUser(user);
                this.props.setIsLoggedIn(flag);
            }
        }
        return flag;
    };

    render() {
        if (!this.isLoggedin()) {
            return <Login />
        }
        return (
            <Dashboard/>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        isLoggedIn: state.app.isLoggedIn
    }
}

const mapDispatchToProps = (dispacth) => {
    return {
        setUser: (user) => dispacth(setUser(user)),
        setIsLoggedIn: (flag) => dispacth(setIsLoggedIn(flag))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(AuthHOC);