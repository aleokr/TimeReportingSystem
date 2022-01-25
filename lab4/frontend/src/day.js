
import moment from "moment";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";

function Day() {
    const [data, setData] = useState();
    const [date, setDate] = useState();

    let history = useHistory();
    useEffect(() => {
        setDate(moment().format("YYYY-MM-DD"));
        async function fetchData() {

            const response = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/day/' + parseInt(localStorage.getItem("userId")) + "/" + moment().format("YYYY-MM-DD").toString()
            );
            setData(response.data);
        }
        fetchData();

    }, []);

    async function loadReport(evt) {
        evt.preventDefault();
        const response = await axios(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/day/' + parseInt(localStorage.getItem("userId")) + "/" + date.toString()
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
        history.push("/editActivity/" + id);
    }

    return (
        <div>
            <div class="main-body">
                <h3>Daily Activities</h3>
                <a class="action-button" href="/addActivity">ADD ACTIVITY</a>
                <h5>Choose date:</h5>
                <input class="date text-box single-line" type="date" onChange={e => setDate(e.target.value)} defaultValue={moment().format("YYYY-MM-DD")} />
                <button class="action-button" type="submit" onClick={loadReport}>SEARCH</button>
                {data && data.length > 0 && <h3>Your entries</h3>}
            </div>
            {data && data.length > 0 && <div>
                <table class="styled-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Code</th>
                            <th>Subcode</th>
                            <th>Time</th>
                            <th>Description</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {data && data.map(data => (
                            <tr>
                                <td>{data.date}</td>
                                <td>{data.code}</td>
                                <td>{data.subcode}</td>
                                <td>{data.time}</td>
                                <td>{data.description}</td>
                                <td>
                                    {!data.confirm &&
                                        <div>

                                            <button class="action-button" onClick={(e) => { deleteActivity(e, data.id) }}>DELETE</button>
                                            <button class="action-button" onClick={(e) => { editActivity(e, data.id) }}>EDIT</button>
                                        </div>
                                    }
                                </td>
                            </tr>
                        ))}


                    </tbody>
                </table>

            </div>}
        </div >
    );
}

export default Day;