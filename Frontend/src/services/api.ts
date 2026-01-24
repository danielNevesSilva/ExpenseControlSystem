import axios from 'axios';

const API_URL = 'https://localhost:7174/api';

console.log('🔧 Configurando API com URL:', API_URL);

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});


api.interceptors.response.use(
  (response) => {
  
    console.groupEnd();
    return response;
  },
  (error) => {
    console.group(`❌ RESPONSE ERROR: ${error.config?.method?.toUpperCase()} ${error.config?.url}`);
    
    if (error.response) {
   
    } else if (error.request) {
   
    } else {
     
    }
    
    console.groupEnd();
    return Promise.reject(error);
  }
);

export const testConnection = async () => {
  try {
    const response = await api.get('/person');
    return { success: true, data: response.data };
  } catch (error) {
    return { success: false, error };
  }
};

export default api;