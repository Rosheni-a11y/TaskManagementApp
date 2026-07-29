import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import SignupView from '../views/SignupView.vue';
import DashboardView from '../views/DashboardView.vue';
import ResetPasswordView from '../views/ResetPasswordView.vue';
import AddTaskView from '../views/AddTaskView.vue';

const routes = [
    { path: '/', name: 'login', component: LoginView },
    { path: '/signup', name: 'signup', component: SignupView },
    { path: '/dashboard', name: 'dashboard', component: DashboardView },
    { path: '/reset-password', name: 'reset-password', component: ResetPasswordView },
    { path: '/add-task', name: 'add-task', component: AddTaskView }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;