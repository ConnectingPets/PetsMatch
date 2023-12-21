import React from 'react';

import { IPossibleSwipes } from '../../interfaces/Interfaces';

import './PetProfile.scss';

interface PetProfileProps {
    pet: IPossibleSwipes
}

const PetProfile: React.FC<PetProfileProps> = ({ pet }) => {
    return (
        <p style={{ padding: '8rem' }}>{pet.name}</p>
    );
};

export default PetProfile;