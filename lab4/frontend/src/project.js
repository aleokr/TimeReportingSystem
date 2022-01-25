import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom"; 

function Project() {
    const [data, setData] = useState();

    let history = useHistory();

    useEffect(() => {
        async function fetchData() {

            const response = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/projects/' + parseInt(localStorage.getItem("userId"))
            );
            setData(response.data);
        }
        fetchData();

    }, []);

    async function editActivity(evt, id) {
        evt.preventDefault();
        history.push("/editActivity/" + id);
    }
    
    async function loadProjects() {

        const response = await axios(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/reports/projects/' + parseInt(localStorage.getItem("userId"))
        );
        setData(response.data);
    }

    async function closeProject(evt, code) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/projects/close/' + code, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        }
        )
            .then(() => {

                loadProjects(evt);
            });
    }

    return (
        <div>
            <div class="main-body">
                <a class="action-button" href="/addProject">ADD NEW PROJECT</a>
                {data !== null && <div>
                    <h3>Your projects</h3>
                    <table class="styled-table">

                        {data && data.map(project => (
                            <div>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>{project.code}</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td></td>
                                        <td>
                                            {project.active &&
                                                <div>

                                                    <button class="action-button" onClick={(e) => { closeProject(e, project.code) }}>CLOSE PROJECT</button>
                                                </div>
                                            }
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Free Budget</td>
                                        <td>{project.budget}</td>
                                    </tr>
                                    <tr>
                                        <td>Users</td>
                                        <td>
                                            <table class="styled-table">

                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Time</th>
                                                        <th>Date</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {project && project.entries && project.entries.length > 0 && project.entries.map(user => (
                                                        <tr>
                                                            <td>{user.name}</td>
                                                            <td>{user.time}</td>
                                                            <td>{user.date}</td>
                                                            <td>
                                                                {!user.canEdit && !user.accepted &&
                                                                    <div>
                                                                        <button class="action-button" onClick={(e) => { editActivity(e, user.id) }}>ACCEPT</button>
                                                                    </div>
                                                                }
                                                                {user.accepted &&
                                                                    <div>
                                                                        <button class="inactive-button" >ACCEPTED</button>
                                                                    </div>
                                                                }
                                                            </td>
                                                        </tr>
                                                    ))}

                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </div>

                        ))}
                    </table>
                </div>
                }


            </div>
        </div>
    );
}

export default Project;