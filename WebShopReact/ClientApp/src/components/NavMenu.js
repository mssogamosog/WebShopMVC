import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  displayName = NavMenu.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>WebShop</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                <Glyphicon glyph='th-list' /> Products
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/CartProducts'} exact>
                            <NavItem>
                                <Glyphicon glyph='th-list' /> CartProducts
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/Customer'}>
                            <NavItem>
                                <Glyphicon glyph='user' /> User
                             </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/Login'}>
                            <NavItem>
                                <Glyphicon glyph='login' /> Login
                             </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}