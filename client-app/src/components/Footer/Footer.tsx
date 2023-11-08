import React from 'react';

import { BiCopyright } from 'react-icons/bi';
import './Footer.scss';

interface FooterProps { }

const Footer: React.FC<FooterProps> = () => {
    return (
        <footer>
            <span><BiCopyright /> Pets Match</span>
        </footer>
    );
};

export default Footer;