import { createApp } from 'vue'
import App from './App.vue'
import {createRouter, createWebHashHistory} from 'vue-router'
import TheContainerStatistic from './components/view/statistic/TheContainerStatistic.vue'
import TheContainerEmployee from './components/view/employee/TheContainerEmployee.vue'
import TheContainerDepartment from './components/view/department/TheContainerDepartment.vue'
import TheContainerPosition from './components/view/position/TheContainerPosition.vue'
import TheContainerTotal from './components/view/total/TheContainerTotal.vue'
import TheHome from './components/view/TheHome.vue'
import TheLogIn from './components/view/TheLogIn.vue'
//import material-icon scss
import "font-awesome/css/font-awesome.min.css";

const routes = [
    
    { path: '/login', component:  TheLogIn},
    { path: '/home', component:  TheHome,
        children: [
          { path: '/total', component: TheContainerTotal},
          { path: '/statistic', component:  TheContainerStatistic},
          { path: '/employee', component:  TheContainerEmployee},
          { path: '/department', component:  TheContainerDepartment},
          { path: '/position', component:  TheContainerPosition},
        ],
      
    },
    { path: '/', component:  App},
  ]

const router = createRouter({
    history: createWebHashHistory(), 
    routes: routes, 
})

const app = createApp(App);
app.use(router)
app.component('VueFontawesome', require('vue-fontawesome-icon/VueFontawesome.vue').default);
app.mount('body')

