<template>
    <div>
        <h2>Tasks</h2>
        <ul>
            <li v-for="task in tasks" :key="task.id">
                {{ task.title }} - {{ task.status }}
                <select :value="task.status" @change="handleUpdateStatus(task.id, $event.target.value)">
                    <option value="Pending">Pending</option>
                    <option value="In Progress">In Progress</option>
                    <option value="Completed">Completed</option>
                </select>
                <button @click="handleDeleteTask(task.id)">Delete</button>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { getTasksByUserId ,deleteTask, updateTaskStatus } from '../services/api';
    

    export default {
        name: 'TaskList',
        props: {
            userId: {
                type: [String, Number],
                required: true
            }
        },
        data() {
            return {
                tasks: [] as any[]
            };
        },
        watch: {
            userId(newUserId: string | number) {
                this.fetchTasks(newUserId);
            }
        },
        methods: {
            async fetchTasks(userId: string | number) {
                if (!userId) {
                    this.tasks = [];
                    return;
                }
                this.tasks = await getTasksByUserId(Number(userId));
            },
            async handleDeleteTask(taskId) {
                const token = localStorage.getItem('authToken');
                await deleteTask(taskId, token);
                this.fetchTasks(this.userId);
            },

            async handleUpdateStatus(taskId, newStatus){
                    const token = localStorage.getItem('authToken');
                    await updateTaskStatus(taskId, newStatus,token);
                    this.fetchTasks(this.userId)
            }



        }
    };
</script>