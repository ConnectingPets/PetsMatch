import React from 'react';

import { BrowserRouter } from 'react-router-dom';

import Routes from './Routes';
import Layout from './components/Layout/Layout';

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Layout>
        <Routes />
      </Layout>
    </BrowserRouter>
  );
};

export default App;
