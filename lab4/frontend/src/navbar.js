import "./css/navBar.css"

import { Container } from "react-bootstrap";
function NavBar() {

    return (
        <div>
            <Container className="menuContainer">
            <nav className="navMenu">
            <a className="links" href="/home">Day</a>
            <a className="links" href="/month">Month</a>
            <a className="links" href="/project">Projects</a>
            <a className="links" href="/" >Logout</a>
            </nav>
            </Container>
        </div>
    );
}

export default NavBar;
