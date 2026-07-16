<template>
    <div>
        <h2>Create New Task</h2>
        <form @submit.prevent="handleCreateTask">
            <div>
                <label for="title">Title:</label>
                <input id="title" type="text" v-model="title" required />
            </div>
            <div>
                <label for="description">Description:</label>
                <input id="description" type="text" v-model="description" />
            </div>

            <label for="dueDate">Due Date:</label>
            <input id="dueDate" type="date" v-model="dueDate" />

            <button type="submit">Add Task</button>
        </form>
    </div>
</template>


<script lang="ts">
    import { createTask } from '../services/api';

    export default {
        name: 'TaskForm',
        props: {
            userId: {
                type: [String, Number],
                required: true
            }
        },
        data() {
            return {
                title: '',
                description: '',
                dueDate: ''

            };
        },
        methods: {
            async handleCreateTask() {
                const task = {
                    title: this.title,
                    description: this.description,
                    status: 'Pending',
                    userId: Number(this.userId),
                    dueDate: this.dueDate || null
                };

                const token = localStorage.getItem('authToken');
                await createTask(task, token);

                this.title = '';
                this.description= '';
                this.dueDate = '';

                this.$emit('task-created')

            }
        }
    };
    </script>