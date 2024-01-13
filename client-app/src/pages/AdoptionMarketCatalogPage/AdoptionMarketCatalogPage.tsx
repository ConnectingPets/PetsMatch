import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import { IUserAnimals } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import Footer from '../../components/Footer/Footer';
import './AdoptionMarketCatalogPage.scss';

interface AdoptionMarketCatalogPageProps { }

const AdoptionMarketCatalogPage: React.FC<AdoptionMarketCatalogPageProps> = observer(() => {
    const [isMarket, setIsMarket] = useState<boolean>(true);
    const [petsInMarket, setPetsInMarket] = useState<IUserAnimals[]>([]);
    const [petsForAdoption, setPetsForAdoption] = useState<IUserAnimals[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const allPetsInMarketResult = await agent.apiMarketplace.getAllAnimalsInMarketplace();
                setPetsInMarket(allPetsInMarketResult.data);

                const allPetsForAdoptionResult = await agent.apiAdoption.getAllAnimalsForAdoption();
                setPetsForAdoption(allPetsForAdoptionResult.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchData();
    }, []);

    const onClickAdoptionMarket = () => {
        setIsMarket(state => !state);
    };

    return (
        <div className={themeStore.isLightTheme ? 'catalog-wrapper' : 'catalog-wrapper catalog-wrapper__dark'}>
            <FormsHeader title='Pets Catalog' />

            <article className={themeStore.isLightTheme ? 'catalog-wrapper__article ' : 'catalog-wrapper__article catalog-wrapper__article__dark '}>
                <section className='catalog-wrapper__article__options'>
                    <div>
                        <h3 className={isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>Marketplace</h3>
                    </div>
                    <div>
                        <h3 className={!isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>For Adoption</h3>
                    </div>
                </section>

                <section className='catalog-wrapper__pets'>
                    {!isMarket && petsForAdoption.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='catalogAdoption' key={x.id} />)}

                    {isMarket && petsInMarket.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='catalogMarket' key={x.id} />)}
                </section>
            </article>

            <Footer />
        </div>
    );
});

export default AdoptionMarketCatalogPage;