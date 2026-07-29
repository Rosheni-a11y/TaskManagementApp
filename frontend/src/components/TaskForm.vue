<template>
    <div class="rounded-2xl p-5 mb-4 glass-card">
        <h2 class="text-lg font-semibold mb-3 text-brand">Create New Task</h2>
        <form @submit.prevent="handleCreateTask" class="space-y-3">
            <div>
                <label for="title" class="block text-sm font-medium mb-1 text-brand">Title</label>
                <input id="title" type="text" v-model="title" required
                    class="w-full px-3 py-2 rounded-lg glass-input" />
            </div>
            <div>
                <label for="description" class="block text-sm font-medium mb-1 text-brand">Description</label>
                <input id="description" type="text" v-model="description"
                    class="w-full px-3 py-2 rounded-lg glass-input" />
            </div>
            <div>
                <label for="dueDate" class="block text-sm font-medium mb-1 text-brand">Due Date</label>
                <input id="dueDate" type="date" v-model="dueDate"
                    class="w-full px-3 py-2 rounded-lg glass-input text-brand" />
            </div>
            <button type="submit" class="w-full font-medium py-2 rounded-lg transition btn-brand">
                Add Task
            </button>
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
                this.description = '';
                this.dueDate = '';

                this.$emit('task-created');
            }
        }
    };
</script>