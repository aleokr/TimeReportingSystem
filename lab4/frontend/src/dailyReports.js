import NavBar from './navbar';
import Day from './day';
import React from 'react';

import './css/table.css'

function DailyReports() {
    
    return (
        <React.Fragment>
        <NavBar />
        <Day/>
      </React.Fragment>
    );
}

export default DailyReports;