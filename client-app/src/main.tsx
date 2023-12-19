// import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'mobx-react';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import userStore from './stores/userStore.ts';
import App from './App.tsx';
import './global-styles/index.scss';

const stores = {
  userStore
};

ReactDOM.createRoot(document.getElementById('root')!).render(
  // <React.StrictMode>
    <Provider {...stores}>
      <App />
      <ToastContainer />
    </Provider>
  // </React.StrictMode>,
);
