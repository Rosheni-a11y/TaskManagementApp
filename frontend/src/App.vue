<template>
    <div>
        <h1>Task Management App</h1>
        <LoginForm @login-success="handleLoginSuccess" />
        <UserSelector @user-selected="handleUserSelected" />
        <TaskForm v-if="selectedUserId" :userId="selectedUserId" @task-created="handleTaskCreated" />
        <TaskList v-if="selectedUserId" :userId="selectedUserId" ref="taskList" />
    </div>
</template>

<script lang="ts">
    import UserSelector from './components/UserSelector.vue';
    import TaskList from './components/TaskList.vue';
    import LoginForm from './components/LoginForm.vue';
    import TaskForm from './components/TaskForm.vue';

    export default {
        name: 'App',
        components: {
            UserSelector,
            TaskList,
            LoginForm,
            TaskForm
        },
        data() {
            return {
                selectedUserId: '',
                authToken: ''
            };
        },
        methods: {
            handleUserSelected(userId: string) {
                this.selectedUserId = userId;
            },
            handleLoginSuccess(token: string) {
                this.authToken = token;
            },
            handleTaskCreated() {
                (this.$refs.taskList as any).fetchTasks(this.selectedUserId);
            }
        }
    };
</script>