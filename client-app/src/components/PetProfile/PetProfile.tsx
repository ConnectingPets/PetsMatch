import React from 'react';
import { useParams } from 'react-router-dom';
import { GiPartyPopper } from 'react-icons/gi';
import { PiDogThin } from 'react-icons/pi';
import { TbGenderMale, TbGenderFemale, TbVaccine } from 'react-icons/tb';
import { FaWeightScale, FaPassport } from 'react-icons/fa6';
import { MdOutlineCastForEducation, MdOutlineTextSnippet } from 'react-icons/md';
import { FaBirthdayCake } from 'react-icons/fa';
import { IoShareSocialOutline } from 'react-icons/io5';
import { toast } from 'react-toastify';

import { IPossibleSwipes } from '../../interfaces/Interfaces';
import themeStore from '../../stores/themeStore';
import chatStore from '../../stores/chatStore';
import agent from '../../api/axiosAgent';

import './PetProfile.scss';

interface PetProfileProps {
    pet: IPossibleSwipes,
    onUnmatch: () => void
}

const PetProfile: React.FC<PetProfileProps> = ({ pet, onUnmatch }) => {
    const { id: petId } = useParams();
    let birthDate;

    if (pet.birthDate) {
        const inputDate = new Date(pet.birthDate);
        const year = inputDate.getFullYear();
        const month = String(inputDate.getMonth() + 1).padStart(2, '0');
        const day = String(inputDate.getDate()).padStart(2, '0');
        birthDate = `${day}-${month}-${year}`;
    }

    const onUnmatchClick = async () => {
        try {
            const petTwoId = pet.unmatchId;

            const res = await agent.apiMatches.unmatch(petId!, petTwoId!);

            if (res.isSuccess) {
                toast.success(res.successMessage);

                onUnmatch();
                chatStore.hideChat();
            } else {
                toast.error(res.errorMessage);
            }
        } catch (err) {
            console.log(err);

            toast.error('An error occurred while processing the request');
        }
    };

    return (
        <div className={themeStore.isLightTheme ? 'profile-wrapper' : 'profile-wrapper profile-wrapper__dark'}>
            <div className="profile-wrapper__img">
                <img src={pet.photo} alt={`${pet.name}'s photo`} />
            </div>

            <div className="profile-wrapper__info">
                <p className="profile-wrapper__info__name">{pet.name}</p>
                <p><GiPartyPopper />{pet.age}</p>
                <p><PiDogThin /> {pet.breed}</p>
                <p>{pet.gender == 'Male' ? <TbGenderMale /> : <TbGenderFemale />} {pet.gender}</p>
                {pet.weight && <p><FaWeightScale /> {pet.weight}</p>}
                {pet.description && <p><MdOutlineTextSnippet /> {pet.description}</p>}
                <p><TbVaccine /> {pet.healthStatus}</p>
                <p><MdOutlineCastForEducation /> {`Educated: ${pet.isEducated ? 'Yes' : 'No'}`}</p>
                <p><FaPassport /> {`Valid documents: ${pet.isHavingValidDocuments ? 'Yes' : 'No'}`}</p>
                {pet.birthDate && <p><FaBirthdayCake /> {birthDate}</p>}
                {pet.socialMedia && <p><IoShareSocialOutline /> {pet.socialMedia}</p>}
            </div>

            {pet.unmatchId && (
                <button onClick={onUnmatchClick}>UNMATCH</button>
            )}
        </div>
    );
};

export default PetProfile;