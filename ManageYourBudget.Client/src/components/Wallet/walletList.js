import React from 'react';
import { Grid, Header } from 'semantic-ui-react';
import WalletMiniature from './walletMiniature';

const WalletList = ({wallets, title, onDelete, changeFavorite}) => {

    const WalletsToDisplay = () => (
        <div style={{paddingTop: '20px'}}>
            <Header as='h3' dividing>{title}</Header>
            <Grid>
                {wallets.map((wallet, index) => {
                    return (
                        <Grid.Column key={index} mobile={16} tablet={8} computer={5}>
                            <WalletMiniature changeFavorite={changeFavorite} key={wallet.id} wallet={wallet} onDelete={onDelete}/>
                        </Grid.Column>
                    )
                })}
            </Grid>
        </div>
    );

    return (
        <div className="wallet-list">
            {wallets.length ? <WalletsToDisplay/> : null}
        </div>
    )
};

export default WalletList;