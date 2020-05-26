import React from 'react';
import { Card, Flag, Icon } from 'semantic-ui-react';
import { walletHelper } from '../../common/index';
import { routesConstants } from '../../routing';

const WalletMiniature = ({wallet, onDelete, changeFavorite}) => {
    const deleteWallet = (event) => {
        event.preventDefault();
        onDelete(wallet);
    };
    const changeWalletFavorite = (event) => {
        event.preventDefault();
        changeFavorite(wallet);
    };
    const urlToWallet = `${routesConstants.WALLET}/${wallet.id}`;
    return (
        <Card href={urlToWallet} style={{minHeight: '10vh'}} centered fluid>
            <Card.Content>
                <Card.Header>
                    <div className="row-space-between">
                        <div>
                            {wallet.name}
                        </div>
                        <div>
                            <Icon onClick={deleteWallet} link name='close'/>
                        </div>
                    </div>
                </Card.Header>
                <Card.Meta>
                    {`${wallet.category}, currency: ${wallet.defaultCurrency}`}
                    <Flag className="wallet-icon" name={walletHelper.currencyToFlagCode(wallet.defaultCurrency)}/>
                </Card.Meta>
                <div style={{marginRight: '-2px'}} className="aligned-right">
                    <Icon onClick={changeWalletFavorite} color={wallet.favorite ? 'yellow' : null} size="large" link
                          name="star"/>
                </div>
            </Card.Content>
        </Card>
    )
};

export default WalletMiniature;