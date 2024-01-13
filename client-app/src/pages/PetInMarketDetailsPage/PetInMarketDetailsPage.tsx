import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import agent from '../../api/axiosAgent';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import Footer from '../../components/Footer/Footer';
import './PetInMarketDetailsPage.scss';

interface PetInMarketDetailsPageProps { }

const PetInMarketDetailsPage: React.FC<PetInMarketDetailsPageProps> = observer(() => {
    const { petId } = useParams();

    useEffect(() => {
        agent.apiMarketplace.getAnimalWithUserInfoByAnimalId(petId!)
            .then(res => {
                console.log(res.data);
            });
    }, [petId]);

    return (
        <div className={themeStore.isLightTheme ? 'details-wrapper' : 'details-wrapper details-wrapper__dark'}>
            <FormsHeader title={'Name\'s page'} />

            <article className={themeStore.isLightTheme ? 'details-wrapper__article ' : 'details-wrapper__article details-wrapper__article__dark '}>

            </article>

            <Footer />
        </div>
    );
});

export default PetInMarketDetailsPage;
