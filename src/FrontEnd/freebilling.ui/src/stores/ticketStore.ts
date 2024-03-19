import { defineStore } from 'pinia'
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';
import type { AxiosError } from 'axios';

import { type Customer } from "@/models/Customer";
import type { Ticket } from '@/models/Ticket';
import type { Employee } from '@/models/Employee';
import type { Project } from '@/models/Project';



