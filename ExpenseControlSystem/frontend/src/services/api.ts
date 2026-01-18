// frontend/src/services/api.ts - VERSÃO COM DEBUG DETALHADO
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


// LOG DETALHADO DE RESPONSES
api.interceptors.response.use(
  (response) => {
    // console.group(`✅ RESPONSE ${response.status}: ${response.config.method?.toUpperCase()} ${response.config.url}`);
    // console.log('Status:', response.status, response.statusText);
    // console.log('Headers:', response.headers);
    // console.log('Data:', response.data);
    console.groupEnd();
    return response;
  },
  (error) => {
    console.group(`❌ RESPONSE ERROR: ${error.config?.method?.toUpperCase()} ${error.config?.url}`);
    
    if (error.response) {
      // O servidor respondeu com status de erro
    //   console.error('Status:', error.response.status);
    //   console.error('Status:', error.response.status);
    //   console.error('Status Text:', error.response.statusText);
    //   console.error('Headers:', error.response.headers);
    //   console.error('Data:', error.response.data);
    //   console.error('Request URL:', error.config?.baseURL + error.config?.url);
    //   console.error('Request Method:', error.config?.method);
    //   console.error('Request Headers:', error.config?.headers);
    } else if (error.request) {
      // A requisição foi feita mas não houve resposta
    //   console.error('No response received');
    //   console.error('Request:', error.request);
    //   console.error('Message:', error.message);
    } else {
      // Algo aconteceu na configuração da requisição
    //   console.error('Request setup error:', error.message);
    //   console.error('Config:', error.config);
    }
    
    console.groupEnd();
    return Promise.reject(error);
  }
);

// Teste de conexão imediata
export const testConnection = async () => {
  try {
    const response = await api.get('/person');
    return { success: true, data: response.data };
  } catch (error) {
    return { success: false, error };
  }
};

export default api;