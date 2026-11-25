using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopContentBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<ShopItem> shopItemList;
    [SerializeField] private Image itemImagePrefab;
    [SerializeField] private GameObject itemTilePrefab;
    [SerializeField] private GameObject textPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(ShopItem item in shopItemList)
        {
            makeGridTile(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void makeGridTile(ShopItem item)
    {
        Vector3 scaleDownVector = new Vector3(0.7f, 0.7f, 0.7f);

        GameObject itemTile = Instantiate(itemTilePrefab);

        GameObject textPrice = Instantiate(textPrefab);
        textPrice.GetComponent<TMPro.TextMeshProUGUI>().text = item.price.ToString();

        textPrice.transform.localScale = scaleDownVector;

        if(item.owned == true)
        {
            textPrice.gameObject.SetActive(false);
        }

        itemTile.GetComponent<Button>().onClick.AddListener(() => buyItem(item, textPrice));

        Image itemImage = Instantiate(itemImagePrefab);

        itemImage.GetComponent<Image>().color = item.color;

        itemImage.transform.position = transform.position;

        itemImage.transform.SetParent(itemTile.transform);

        itemImage.transform.position = new Vector3(0, 0, 0);

        itemImage.transform.localScale = scaleDownVector;

        GameObject textName = Instantiate(textPrefab);
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = item.name;

        textName.transform.localScale = scaleDownVector;

        textName.transform.SetParent(itemTile.transform);

        textPrice.transform.SetParent(itemTile.transform);

        itemTile.transform.SetParent(transform);
    }

    private void buyItem(ShopItem item, GameObject price)
    {
        if(AutoloadUnity.cash < item.price && !item.owned)
        {
            return;
        }

        if(item.owned == false)
        {
            AutoloadUnity.cash -= item.price;
            item.owned = true;
            price.gameObject.SetActive(false);
        }
        itemPrefab.GetComponent<SpriteRenderer>().color = item.color;
    }
}
