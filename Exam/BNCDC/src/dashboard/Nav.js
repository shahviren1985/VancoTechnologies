import React from 'react';

const Nav = (props) => <nav className="no-print navbar has-background-dark-pink" role="navigation" aria-label="main navigation">
<div className="navbar-brand">
    <a className="navbar-item" onClick={props.reload} >
       <h1 className="subtitle has-text-white">{props.user.system_title}</h1>
    </a>
</div>
<div  className="navbar-menu">
    <div className="navbar-end">
        <h1 className="navbar-item has-text-white subtitle">Exam System</h1>
    </div>
    <div className="navbar-end">
        <div className="navbar-item">
            <div className="buttons">
                <a className="has-text-white " onClick={props.logout}>
                    <strong>Logout</strong>
                </a>
            </div>
        </div>
    </div>
</div>
</nav>

export default Nav;