import React, {useEffect, useState} from "react"
import {ScrollView} from "react-native-gesture-handler"
import {Assets, Chip, ExpandableSection, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {getRestaurantById} from "../../api"
import {DecreaseType, Restaurant} from "../../api/interfaces"
import {PriceIndicator} from "../../components/price-indicator"
import {navigateToRestaurantInfo} from "../../services/navigation"
import {formatPrice} from "../../utils/currency"
import {convertMinsToHrsMins, formatDate, formatTime, timeAgoFull} from "../../utils/date"
import {FoodInfoProps} from "./interfaces"

export const FoodInfo = ({componentId, food, showRestaurantLink = true}: FoodInfoProps) => {
  const [restaurant, setRestaurant] = useState({} as Restaurant)
  const [descriptionExpanded, setDescriptionExpanded] = useState(true)

  const {
    name,
    description,
    typesOfFood,
    decreaseType,
    intervalTimeInMinutes,
    currentPrice,
    startingPrice,
    minimumPrice,
    idRestaurant,
    createdAt,
    amountPerInterval,
    percentPerInterval,
    startDecreasingAt,
    imageURL
  } = food

  const fetchRestaurant = async () => {
    setRestaurant((await getRestaurantById(idRestaurant)) as Restaurant)
  }

  useEffect(() => {
    fetchRestaurant()
  }, [])

  return (
    <ScrollView>
      <View margin-s4>
        <View centerH>
          <Text text30M purple20 marginT-s2 marginB-s1>
            {name}
          </Text>
          <View row centerH style={{flexWrap: "wrap"}}>
            {typesOfFood.map(type => (
              <Chip margin-s1 key={type.id} label={type.name} />
            ))}
          </View>
          <Image marginT-s2 source={{uri: imageURL, height: 220, width: 350}} style={{height: 220, width: 350}} />
        </View>
        <View marginT-s6 marginH-s6>
          {showRestaurantLink ? (
            <TouchableOpacity
              row
              centerV
              onPress={() => {
                navigateToRestaurantInfo(componentId, {componentId, restaurant})
              }}
            >
              <Text text60L purple20 style={{width: 120}}>
                Restaurant
              </Text>
              <Text text60L>{`${restaurant.name} ↗️`}</Text>
            </TouchableOpacity>
          ) : (
            <View row centerV>
              <Text text60L purple20 style={{width: 120}}>
                Restaurant
              </Text>
              <Text text60L>{restaurant.name}</Text>
            </View>
          )}
          <View row centerV marginT-s4>
            <Text text60L purple20 style={{width: 120}}>
              Cooked
            </Text>
            <View>
              <Text text60L style={{width: 200}}>
                {`${timeAgoFull(createdAt)}`}
              </Text>
              <Text text80L style={{width: 250}}>{`${formatDate(createdAt)} | ${formatTime(createdAt)}`}</Text>
            </View>
          </View>
          <View row centerV marginT-s4>
            <Text text60L purple20 style={{width: 120}}>
              Price
            </Text>
            <Text text60L green10={currentPrice !== startingPrice} text60M={currentPrice !== startingPrice}>
              {formatPrice(currentPrice)}
            </Text>
          </View>
          <View marginT-s2>
            <PriceIndicator currentPrice={currentPrice} minimumPrice={minimumPrice} maximumPrice={startingPrice} />
            <View marginT-s2>
              <Text text80L>
                The price of <Text text80M>{name}</Text> decreases by{" "}
                <Text text80M>
                  {decreaseType === DecreaseType.AMOUNT ? formatPrice(amountPerInterval) : `${percentPerInterval}%`}
                </Text>{" "}
                every <Text text80M>{convertMinsToHrsMins(intervalTimeInMinutes)}</Text> starting at{" "}
                <Text text80M>{`${formatDate(startDecreasingAt)}, ${formatTime(startDecreasingAt)}`}</Text>.
              </Text>
            </View>
          </View>
          <View centerV marginT-s4>
            <ExpandableSection
              sectionHeader={
                <View row>
                  <Text text60L purple20 marginR-s2>
                    Description
                  </Text>
                  <Image
                    style={{
                      alignSelf: "center",
                      width: 16,
                      height: 16,
                      transform: [{scaleY: descriptionExpanded ? -1 : 1}]
                    }}
                    source={require("../../../assets/down-chevron.png")}
                  />
                </View>
              }
              expanded={descriptionExpanded}
              onPress={() => setDescriptionExpanded(!descriptionExpanded)}
            >
              <Text marginT-s1 text80L>
                {description}
              </Text>
            </ExpandableSection>
          </View>
        </View>
      </View>
    </ScrollView>
  )
}
