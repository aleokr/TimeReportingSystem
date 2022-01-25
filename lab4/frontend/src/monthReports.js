import NavBar from './navbar';
import Month from './month';
import React from 'react';

import './css/table.css'

function MonthReports() {
    
    return (
        <React.Fragment>
        <NavBar />
        <Month/>
      </React.Fragment>
    );
}

export default MonthReports;