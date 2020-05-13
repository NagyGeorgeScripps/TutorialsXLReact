import * as React from 'react';
import { Route, Router } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import { BrowserRouter } from 'react-router-dom';

import './custom.css'

console.log("URL: " + process.env.PUBLIC_URL);

export default () => (
	<Layout>
		<BrowserRouter basename={'/reactwebapitest'}>
			<Route path={`${process.env.PUBLIC_URL}/counter`} component={Counter} />
			<Route path={`${process.env.PUBLIC_URL}/fetch-data/:startDateIndex?`} component={FetchData} />
			<Route exact path={`${process.env.PUBLIC_URL}/`} component={Home} />
		</BrowserRouter>
	</Layout>
);
