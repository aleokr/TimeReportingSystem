import "./css/form.css"
import { useParams } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";

function EditActivity() {
    const params = useParams();

    let history = useHistory();

    const [activityId, setActivityId] = useState();
    const [username, setUsername] = useState();
    const [projectName, setProjectName] = useState();
    const [date, setDate] = useState();
    const [time, setTime] = useState();
    const [description, setDescription] = useState();
    const [userId, setUserId] = useState();

    useEffect(() => {
        async function fetchData() {

            const response = await axios(
                process.env.REACT_APP_BACKEND_BASE_URL + '/api/activities/' + params.id
            );
            setActivityId(response.data.id);
            setUsername(response.data.user.name + ' ' + response.data.user.surname);
            setProjectName(response.data.project.code);
            setDate(response.data.date);
            setTime(response.data.time);
            setDescription(response.data.description);
            setUserId(response.data.user.id);
        }
        fetchData();

    }, []);
    async function editActivity(evt) {
        evt.preventDefault();
        fetch(
            process.env.REACT_APP_BACKEND_BASE_URL + '/api/activities/update', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ActivityId: activityId,
                Description: description,
                Submit: parseInt(localStorage.getItem("userId")) !== userId,
                Time: time
            })
        }
        )
            .then(() => {

                history.goBack();

            });

    }
    return (
        <div class="form-box">
            <h2>Edit activity</h2>
            <form onSubmit={editActivity}>
                <label>Username</label>
                <div class="user-box">
                    <input type="text" defaultValue={username} readOnly />
                </div>
                <label>Project name</label>
                <div class="user-box">
                    <input type="text" defaultValue={projectName} readOnly />
                </div>
                <label>Date</label>
                <div class="user-box">
                    <input type="text" defaultValue={date} readOnly />
                </div>
                <div class="user-box">
                    <input type="number" defaultValue={time} onChange={e => setTime(e.target.value)} required />
                    <label>Time</label>
                </div>
                {parseInt(localStorage.getItem("userId")) === userId &&
                    <div class="user-box">
                        <input type="text" defaultValue={description} onChange={e => setDescription(e.target.value)} />
                        <label>Description</label>
                    </div>
                }

                {parseInt(localStorage.getItem("userId")) !== userId &&
                    <div>
                        <label>Description</label>
                        <div class="user-box">
                            <input type="text" defaultValue={description} readOnly />

                        </div>
                    </div>

                }

                <button class="form-button" type="submit">Submit</button>
            </form>

            <a class="form-button" href="/home">Cancel</a>
        </div>
    );
}

export default EditActivity;