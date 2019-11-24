import React from 'react';
import { connect } from 'react-redux';
import { getUsers } from '../store/actions/users';
import { setUserInLocalStorage, getUserFromLocalStorage } from '../util';
import { setUser, setIsLoggedIn } from '../store/actions/app';

class Login extends React.Component {

    constructor(props) {
        super(props);
        this.state = { username: '', password: '', errors: {} }
        this.onChange = this.onChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    onChange(e) {
        this.setState(({
            [e.target.name]: e.target.value
        }))
    }

    submit() {
        this.setState({ errors: {} })
        const { users } = this.props;
        const { username, password } = this.state;
        if (!this.checkErrors()) {
            const userName = `${username}`.toLowerCase();
            const user = users.find((user) => user.user === userName && password === user.password);
            if (user) {
                this.logIn(user);
            }
            else {
                const errors = {};
                errors.login = 'Username or password is incorrect.'
                this.setState({ errors })
            }

        }
    }

    logIn(user) {
        setUserInLocalStorage(user);
        this.props.setUser(user);
        this.props.setIsLoggedIn(true);
    }

    checkErrors() {
        const errors = {};
        let hasError = false;
        const { username, password } = this.state;
        if (!username) {
            errors.username = 'Username is required';
        }
        if (!password) {
            errors.password = 'Password is required'
        }
        if (Object.keys(errors).length > 0) {
            hasError = true;
            this.setState({ errors });
        }
        return hasError;
    }

    componentDidMount() {
        this.props.getUsers();
    }

    isLoggedIn() {
        if (!this.props.isLoggedIn) {
            const user = getUserFromLocalStorage();
            return !!user;
        }
        return true;
    }

    render() {
        const { username, password, errors } = this.state;

        return (
            <div className="has-background-grey-lighter full-height">
                <nav className="navbar has-background-dark-pink" role="navigation" aria-label="main navigation">
                    <div className="navbar-menu">
                        <div className="center-title">
                            <h1 className="navbar-item has-text-white subtitle">Exam System</h1>
                        </div>
                    </div>
                </nav>
                <div className="section">
                    <div className="columns mt-10">
                        <div className="column is-one-third"></div>
                        <div className="column  is-one-third">
                            <div className="field">
                                <p className="control">
                                    <input className="input" name="username" type="text" value={username} placeholder="Username" onChange={this.onChange} />
                                    <span className="has-text-danger">{errors.username}</span>
                                </p>
                            </div>
                            <div className="field">
                                <p className="control">
                                    <input className="input" name="password" type="password" value={password} placeholder="Password" onChange={this.onChange} />
                                    <span className="has-text-danger">{errors.password}</span>
                                </p>
                            </div>
                            <div className="field">
                                <p className="control">
                                    <button className="button is-success" onClick={this.submit}>Login</button>
                                </p>
                                <span className="has-text-danger">{errors.login}</span>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        )
    }
}

const mapStateToProps = (state) => {
    return {
        users: state.users,
        isLoggedIn: state.app.isLoggedIn
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getUsers: () => dispatch(getUsers()),
        setUser: (user) => dispatch(setUser(user)),
        setIsLoggedIn: (flag) => dispatch(setIsLoggedIn(flag))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Login);