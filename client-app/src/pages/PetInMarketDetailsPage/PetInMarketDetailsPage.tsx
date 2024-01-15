import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import { IAnimalWithUserInfo } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';
import { GenderEnum, HealthStatusEnum, genderEnum, healthStatusEnum } from '../../utils/constants';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import Footer from '../../components/Footer/Footer';
import './PetInMarketDetailsPage.scss';

interface PetInMarketDetailsPageProps { }

const PetInMarketDetailsPage: React.FC<PetInMarketDetailsPageProps> = observer(() => {
    const [pet, setPet] = useState<IAnimalWithUserInfo | undefined>(undefined);
    const [largePhoto, setLargePhoto] = useState<string>('');
    const { petId } = useParams();

    useEffect(() => {
        agent.apiMarketplace.getAnimalWithUserInfoByAnimalId(petId!)
            .then(res => {
                setPet(res.data);
            });
    }, [petId]);

    const gender = Object.keys(genderEnum).filter(x => genderEnum[x as keyof GenderEnum] == pet?.gender)[0];
    const healthStatus = Object.keys(healthStatusEnum).filter(x => healthStatusEnum[x as keyof HealthStatusEnum] == pet?.healthStatus)[0];

    if (pet?.birthDate) {
        const inputDate = new Date(pet?.birthDate);
        const year = inputDate.getFullYear();
        const month = String(inputDate.getMonth() + 1).padStart(2, '0');
        const day = String(inputDate.getDate()).padStart(2, '0');
        const birthDate = `${day}-${month}-${year}`;

        pet.birthDate = birthDate;
    }

    const onCloseLargePhoto = () => {
        setLargePhoto('');
    };

    return (
        <div className={themeStore.isLightTheme ? 'details-wrapper' : 'details-wrapper details-wrapper__dark'}>
            <FormsHeader title={`${pet?.name}'s page`} />

            <article className={themeStore.isLightTheme ? 'details-wrapper__content ' : 'details-wrapper__content details-wrapper__content__dark '}>
                <div className="details-wrapper__content__info">
                    <section>
                        <h2>Basic Information</h2>
                        <p><b>Breed:</b> {pet?.breedName}</p>
                        <p><b>Gender:</b> {gender}</p>
                        <p><b>Age:</b> {pet?.age}</p>
                        {pet?.weight && <p><b>Weight:</b> {pet?.weight} kg</p>}
                        <p><b>Health Status:</b> {healthStatus}</p>
                        <p><b>Is Educated:</b> {pet?.isEducated ? 'Yes' : 'No'}</p>
                        <p><b>Has Valid Documents:</b> {pet?.isHavingValidDocuments ? 'Yes' : 'No'}</p>
                        {pet?.birthDate && <p><b>Birth Date:</b> {pet?.birthDate}</p>}
                    </section>

                    <section>
                        <h2>Description</h2>
                        {pet?.description && <p className="text">{pet?.description}</p>}
                    </section>

                    <section>
                        {pet?.price && (
                            <div>
                                <h2>Price</h2>
                                <p><b>Price:</b> ${pet?.price}</p>
                            </div>
                        )}
                        {pet?.socialMedia && (
                            <div className="media">
                                <h2>Social Media</h2>
                                <a href={`${pet.socialMedia}`} target='_blank' rel='noopener noreferrer'>{pet.socialMedia}</a>
                            </div>
                        )}
                        <div>
                            <h2>Contact</h2>
                            <p className="name"><b>Name:</b> {pet?.userName}</p>
                            <p><b>Email:</b> {pet?.userEmail}</p>
                            {pet?.address && <p><b>Address:</b> {pet?.address}</p>}
                        </div>
                    </section>
                </div>
            </article>

            <article className={themeStore.isLightTheme ? 'details-wrapper__content ' : 'details-wrapper__content details-wrapper__content__dark '}>
                <div className="photo-container">
                    {pet?.photos.map((photo, index) => (
                        <img onClick={() => setLargePhoto(photo.url)} key={index} src={photo.url} alt={`${pet.name}'s photo`} />
                    ))}
                </div>
            </article>

            {largePhoto && (
                <div className="details-wrapper__zoom-photo">
                    <div className="details-wrapper__zoom-photo__modal">
                        <img src={largePhoto} alt="pet photo" />
                        <button onClick={onCloseLargePhoto}>X</button>
                    </div>
                </div>
            )}

            <Footer />
        </div>
    );
});

export default PetInMarketDetailsPage;
