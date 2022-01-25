import { BrowserRouter, Route, Switch } from 'react-router-dom';
import './App.css';
import './css/table.css'
import Users from './users';
import DailyReports from './dailyReports';
import MonthReports from './monthReports';
import ProjectReports from './projectReports';
import AddActivity from './addActivity'
import AddProject from './addProject'
import EditActivity from './editActivity'
import CloseMonth from './closeMonth'
import ProjectExist from './projectExist'

function App() {

    return (<BrowserRouter>
            <div >
              <Switch>
                <Route exact path='/' component={Users} />
                <Route exact path='/home' component={DailyReports} />
                <Route exact path='/month' component={MonthReports} />
                <Route exact path='/project' component={ProjectReports} />
                <Route exact path='/addActivity' component={AddActivity} />
                <Route exact path='/addProject' component={AddProject} />
                <Route exact path='/editActivity/:id' component={EditActivity} />
                <Route exact path='/close' component={CloseMonth} />
                <Route exact path='/exist' component={ProjectExist} />
              </Switch>
        </div></BrowserRouter>
      );
}

export default App;
