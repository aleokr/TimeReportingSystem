import moment from "moment";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";

function Month() {
    const [data, setData] = useState();
    const [date, setDate] = useState();


    let history = useHistory();
    useEffect(() => {

        setDate(moment().format("YYYY-MM-DD"));

        async function fetchData() {

            const response = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/month/' + parseInt(localStorage.getItem("userId")) + "/" + moment().format("YYYY-MM-DD").toString()
            );
            setData(response.data);
            console.log(response.data);
        }
        fetchData();

    }, []);

    async function loadReport(evt) {
        evt.preventDefault();
        const response = await axios(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/month/' + parseInt(localStorage.getItem("userId")) + "/" + date.toString()
        );
        setData(response.data);
    }

    async function deleteActivity(evt, id) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/activities/' + id, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        }
        )
            .then(() => {

                loadReport(evt);
            });
    }

    async function editActivity(evt, id) {
        evt.preventDefault();
        localStorage.setItem("date", date);
        history.push("/editActivity/" + id);
    }
    async function submitMonth(evt, id) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/activities/closeMonth/' + parseInt(localStorage.getItem("userId")) + "/" + date.toString(), {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        }
        )
            .then(() => {

                loadReport(evt);
            });
    }


    return (
        <div>
            <div class="main-body">
                <h3>Monthly Activities</h3>
                <a class="action-button" href="/addActivity">ADD ACTIVITY</a>
                {data && !data.frozen && <button class="action-button" onClick={submitMonth}>SUBMIT MONTH</button>}
                <h5>Choose date:</h5>
                <input class="date text-box single-line" type="date" onChange={e => setDate(e.target.value)} defaultValue={moment().format("YYYY-MM-DD")} />
                <button class="action-button" type="submit" onClick={loadReport}>SEARCH</button>

                {data && data.projectActivities && data.projectActivities.length > 0 && <h3>Your entries</h3>}
            </div>
            <div>
                {data && data.projectActivities && data.projectActivities.length > 0 && data.projectActivities.map(data => (
                    <div class="main-body">
                        <h4>{data.code}</h4>
                        <table class="styled-table">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Subcode</th>
                                    <th>Time</th>
                                    <th>Description</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>

                                {data.entries.map(entry => (
                                    <tr>
                                        <td>{entry.date}</td>
                                        <td>{entry.subcode}</td>
                                        <td>{entry.time}</td>
                                        <td>{entry.description}</td>
                                        <td>
                                            {entry.canEdit &&
                                                <div>

                                                    <button class="action-button" onClick={(e) => { deleteActivity(e, entry.id) }}>DELETE</button>
                                                    <button class="action-button" onClick={(e) => { editActivity(e, entry.id) }}>EDIT</button>
                                                </div>
                                            }
                                        </td>
                                    </tr>
                                ))}

                            </tbody>

                        </table>
                    </div>

                ))}
            </div>

            <div class="main-body">
                {data && data.accepted && data.accepted.length > 0 && <h3>Accepted activities</h3>}
            </div>
            {data && data.accepted && data.accepted.length > 0 && <div>
                <table class="styled-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Code</th>
                            <th>Time</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.accepted.map(entry => (
                            <tr>
                                <td>{entry.date}</td>
                                <td>{entry.code}</td>
                                <td>{entry.time}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>}
        </div>

    );
}

export default Month;