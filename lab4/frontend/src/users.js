import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';
import './css/table.css'
import { useHistory } from "react-router-dom";

function Users() {
    const [data, setData] = useState();

    let history = useHistory();
    useEffect(() => {
        async function fetchData() {
            
            const response = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/users/all'
            );
            setData(response.data);
          }
          fetchData();
        
    }, []);

    async function login(e, id) {
        e.preventDefault();
        localStorage.clear()
        localStorage.setItem("userId", id);
        history.push("/home");
    }
    
    return (
        <div class="main-body">
            
                <table class="styled-table">
                    <thead>
                        <tr>
                            <th>Choose user</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data && data.map(user => (
                            <tr>
                                <td>
                                    <button class="action-button" onClick={(e) => { login(e, user.id) }}>{user.name} {user.surname}</button>
                                </td>
                            </tr>
                        ))}

                    </tbody>
                </table>
            
        </div>
    );
}

export default Users;